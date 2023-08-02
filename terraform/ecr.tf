resource "aws_ecr_repository" "signalr_ecr" {
  name = "signalr_ecr"
  force_delete = true

  image_scanning_configuration {
    scan_on_push = true
  }
}

resource "null_resource" "docker_packaging" {
  provisioner "local-exec" {
    command = <<EOF
    cd ../
    aws ecr get-login-password --region eu-west-1 | docker login --username AWS --password-stdin ${data.aws_caller_identity.current.account_id}.dkr.ecr.eu-west-1.amazonaws.com
    dotnet publish -c Release
    docker build -t "${aws_ecr_repository.signalr_ecr.repository_url}:latest" .
    docker push "${aws_ecr_repository.signalr_ecr.repository_url}:latest"
    EOF
  }


  triggers = {
    "run_at" = timestamp()
  }

  depends_on = [
    aws_ecr_repository.signalr_ecr,
  ]
}