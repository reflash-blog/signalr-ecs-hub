resource "aws_s3_bucket" "signalr-bucket" {
  bucket = "signalr-bucket"
  force_destroy = true
}

resource "aws_s3_bucket_public_access_block" "signalr-bucket-public-access" {
  bucket = aws_s3_bucket.signalr-bucket.id

  block_public_acls       = false
  block_public_policy     = false
  ignore_public_acls      = false
  restrict_public_buckets = false
}

resource "aws_s3_bucket_website_configuration" "signalr-bucket-config" {
  bucket = aws_s3_bucket.signalr-bucket.bucket
  index_document {
    suffix = "index.html"
  }
}

resource "aws_s3_bucket_policy" "signalr-policy" {
  bucket = aws_s3_bucket.signalr-bucket.id
  policy = <<EOF
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Sid": "PublicReadGetObject",
      "Effect": "Allow",
      "Principal": "*",
      "Action": "s3:GetObject",
      "Resource": "arn:aws:s3:::${aws_s3_bucket.signalr-bucket.bucket}/*"
    }
  ]
}
EOF
}

resource "null_resource" "website_packaging" {
  provisioner "local-exec" {
    command = <<EOF
    cd ../frontend
    REACT_APP_ALB_URL=${aws_lb.main.dns_name} yarn build
    aws s3 sync ./build s3://${aws_s3_bucket.signalr-bucket.id}
    EOF
  }


  triggers = {
    "run_at" = timestamp()
  }

  depends_on = [
    aws_ecr_repository.signalr_ecr,
    aws_lb.main
  ]
}