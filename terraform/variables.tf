variable "name" {
  description = "the name of your stack, e.g. \"demo\""
  default = "signalr-ecs"
}

variable "environment" {
  description = "the name of your environment, e.g. \"prod\""
  default = "dev"
}

variable "region" {
  description = "the AWS region in which resources are created"
  default = "eu-west-1"
}

variable "container_port" {
  description = "Port of container"
  default = 80
}

variable "private_key" {
  description = "Private key"
  default = <<EOF
-----BEGIN RSA PRIVATE KEY-----
...
-----END RSA PRIVATE KEY-----
EOF
}

variable "certificate" {
  description = "Certificate"
  default = <<EOF
-----BEGIN CERTIFICATE-----
...
-----END CERTIFICATE-----
EOF
}