# MailSender
A simple distributed system made to send emails.

## Architecture

## How to run this project on your machine:
After cloning this project just run the command below:

```
docker-compose up -d
```

After that, you can send a POST request to:
``` http://localhost:8080/api/MailSender ```

with the JSON body:

```
{
  "subject": "title of email",
  "content": "message ",
  "destiny": "e-mail"
}
```
ex:
```
{
  "subject": "Foo",
  "content": "Hi Mary, this is a bar",
  "destiny": "mary@gmail.com"
}
```


## Todo
- [ ] Create front end SPA.
- [ ] Configure a reverse proxy to consume the API.
- [ ] Configure middleware in the API to deal with exceptions.
- [ ] Configure middleware in the Worker to deal with exceptions.
- [ ] Add log service in API.
- [ ] Add log service in Worker.
