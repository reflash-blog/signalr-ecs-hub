In case of any certificate issues
```
dotnet dev-certs https --trust
```

To start the server
```
dotnet run
```

To deploy the infrastructure (don't forget to check you users permissions in AWS - the list of policies used by me is below)
```
CloudWatchLogsFullAccess
AmazonEC2FullAccess
IAMFullAccess
AmazonS3FullAccess
AmazonElasticMapReduceforEC2Role
AmazonEC2ContainerRegistryFullAccess
AWSCertificateManagerFullAccess
AWSCloudFormationFullAccess
AmazonEC2ContainerServiceFullAccess
AmazonElastiCacheFullAccess
```
```
cd terraform
terraform init
terraform apply
```

```
export AWS_ACCESS_KEY_ID=...
export AWS_SECRET_ACCESS_KEY=...
```

change private key and certificate in terraform/variables.tf
use the following cmd to generate a self-signed cert (key.pem and cert.pem files)
Specify `test.example.com` as a Common Name:
`openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -days 365`
Remember the key phrase and convert the key to RSA key (AWS doesn't accept the default format)
`openssl rsa -in key.pem > private-rsa-key.pem`

To test via postman:
Websocket to wss://localhost:7093/MessageHub
Send this message `{"protocol":"json","version":1}` (special character is required)
Join group `{ "type": 1, "target": "JoinGroup", "arguments": ["user-id"] }`

Website is deployed to S3 (see signalr-bucket part in terraform/frontend.tf): https://signalr-bucket.s3-website-eu-west-1.amazonaws.com/