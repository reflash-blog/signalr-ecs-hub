In case of any certificate issues
```
dotnet dev-certs https --trust
```

To start the server
```
dotnet run
```

To deploy the infrastructure
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