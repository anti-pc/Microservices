Program.cs 
------------------------------
builder.Services.AddAutoMapper(typeof(Program));  
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

https://docs.portainer.io/start/install-ce/server/docker/linux  
cmd > docker volume create portainer_data  
cmd > docker run -d -p 8000:8000 -p 9000:9000 --name=portainer --restart=always -v /var/run/docker.sock:/var/run/docker.sock -v portainer_data:/data portainer/portainer-ce  


RabbitMQ
------------------------------
cmd > docker run -d --hostname rmq --name rabbit-server -p 8080:15672 -p 5672:5672 rabbitmq:3-management
default user & pass : guest guest

Containers
-----------------------
> mssqlserver
> postgres
> redis
> mongoDB
> rabbitMQ
> portainer
