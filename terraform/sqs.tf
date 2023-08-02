resource "aws_sqs_queue" "sqs_queue" {
  name                      = "signalr-sqs-queue"
  delay_seconds             = 0
  max_message_size          = 2048
  message_retention_seconds = 900
  receive_wait_time_seconds = 10
}

resource "aws_sqs_queue_policy" "sqs_queue_policy" {
  queue_url = aws_sqs_queue.sqs_queue.id
  policy    = <<POLICY
{
  "Version": "2012-10-17",
  "Id": "sqspolicy",
  "Statement": [
    {
      "Sid": "ECSTaskSQS",
      "Effect": "Allow",
      "Principal": {
          "AWS": "${aws_iam_role.ecs_task_role.arn}"
      },
      "Action": [
          "SQS:ReceiveMessage",
          "SQS:DeleteMessage"
      ],
      "Resource": "${aws_sqs_queue.sqs_queue.arn}"
    }
  ]
}
POLICY
}