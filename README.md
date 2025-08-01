
# 🌐 DotnetFilePortal Gateway

**DotnetFilePortal Gateway** es un servicio desarrollado en **.NET 8** que actúa como puerta de entrada para las peticiones hacia el backend de **DotnetFilePortal API**, implementando autenticación con **JWT Bearer** para validar y proteger el acceso a los recursos del sistema.

---

## 🔐 Características principales

- Enrutamiento centralizado hacia **DotnetFilePortal API**.
- Validación de autenticación JWT firmada.
- Integración con ASP.NET Core 8 y middleware JWT Bearer.
- Configuración mediante variables de entorno (`.env`).
- Totalmente contenerizado con **Docker** y orquestado con **docker-compose**.

---

## ⚙️ Tecnologías utilizadas

- .NET 8
- ASP.NET Core Web API
- Microsoft.AspNetCore.Authentication.JwtBearer
- Docker & Docker Compose
- JWT (JSON Web Tokens)

---

## 🚀 Configuración rápida

### 1. Variables de entorno (.env)

Crea un archivo `.env` en la raíz con:

```env
JWT_KEY=clave_secreta_segura
JWT_ISSUER=https://dotnetfileportal-api
JWT_AUDIENCE=https://dotnetfileportal-client
JWT_DURATION=60
BASE_URL=http://dotnetfileportal_api:5000
```

### 2. Docker Compose

Ejecuta el Gateway usando el archivo `docker-compose.yml`:

```yaml
services:
  gateway:
    build: .
    container_name: dotnetfileportal_gateway
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ASPNETCORE_URLS=http://+:5273
      - Jwt__Key=${JWT_KEY}
      - Jwt__Issuer=${JWT_ISSUER}
      - Jwt__Audience=${JWT_AUDIENCE}
      - Jwt__Duration=${JWT_DURATION}
      - GlobalConfiguration__BaseUrl=${BASE_URL}
    ports:
      - "5001:5273"
    networks:
      fileupload_file_upload_network:
        ipv4_address: 172.16.0.5

networks:
  fileupload_file_upload_network:
    external: true
```

Para levantar el servicio:

```bash
docker-compose up --build
```

### 3. Estructura esperada

```
/Gateway/
 ├── Program.cs
 ├── Startup.cs
 ├── Dockerfile
 ├── docker-compose.yml
 ├── appsettings.json
 ├── .env
 └── README.md
```

---

## 🔁 Flujo de autenticación

1. El cliente envía peticiones HTTP con encabezado `Authorization: Bearer <token>`.
2. El Gateway valida el **token JWT** usando los parámetros definidos (`Key`, `Issuer`, `Audience`).
3. Si es válido, reenvía la petición hacia la **API protegida DotnetFilePortal API**.
4. Si el token no es válido o ha expirado, se responde con HTTP 401.

---

## ✅ Recomendaciones

- Rotar periódicamente la `JWT_KEY` y mantenerla segura.
- Puedes extender el Gateway para incluir **Rate Limiting**, **CORS** o **Cache**.

---

## 🛠️ En desarrollo

Este Gateway puede evolucionar para incluir:

- 🧠 Balanceo de carga
- 🔐 Autorización por rol desde Claims
- 📊 Logs distribuidos
- 💬 WebSocket passthrough (para SignalR)

---

## 📝 Licencia

MIT — Puedes usar este proyecto para propósitos educativos y empresariales.

---

## 🙌 Autor

**Julian Dario Gonzalez Toledo**  
📧 juliant.2001@outlook.com 
🔗 [LinkedIn](https://www.linkedin.com/in/julian-dario-gonzalez-toledo-402482223/)
