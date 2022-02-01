# MailSender
A simple distributed system made to send emails.

## Architecture
![alt text](https://github.com/hd1fernando/MailSender/blob/main/docs/Untitled%20Diagram.drawio.png?raw=true)


## How to run this project on your machine:
After cloning this project you need to enter your email configuration in the file settins/appsettings.json in the section 'MailSettingsOptions'.
ex:
```
 "MailSettingsOptions": {
    "Domain": "smtp.gmail.com",
    "Port": "587",
    "UserEmail": "testeemail@gmail.com",
    "UserPassword": "d&!gct3qrLbd#B!%62@mOUP1qkd4MiQVoNqKhRmn60IqbF&Va2u!BiY7nUQRhKakO@lNgivtu45yL#GF7!DB%kgVve0P^yKYFNe"
  },
```

After that, just execute the docker-compose command:
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


## TODO
- [ ] Create front end SPA.
- [ ] Configure a reverse proxy to consume the API.
- [ ] Configure middleware in the API to deal with exceptions.
- [ ] Configure middleware in the Worker to deal with exceptions.
- [ ] Add log service in API.
- [ ] Add log service in Worker.
