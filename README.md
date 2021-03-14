# Örnek Tye Uygulaması

Demoda bir frontend, bir backend birde Redis mevzu bahis. Starcups isimli bir kahve firması var. HeadOffice isimli web arayüzünden İstanbul'un semtlerdeki kahve dükkanlarından gelen malzeme taleplerini görebiliyoruz. 
Malzeme bilgileri StockCollector isimli RestAPI servisi üstünden geliyor. Redis, StockCollector'un çektiği veriyi belli süre cache'lemek için sisteme dahil edilmiş durumda. 

Amaç, Tye aracı ile bu servislerin kolayca ayağa kaldırılması, zahmetsizce dockerize edilmesi, loglarına bakılması, çevre değişkenlerinin yaml bazlı yönetilmesi ve Kubernetes tarafına en basit şekliyle Deploy edilmesi gibi işlemlerin incelenmesi.

_Platform : Windows 10_

## Gerekenler

```
# Sisteme tye yüklemek için aşağıdaki terminal komutu kullanılabilir(Son sürüme bakmak lazım. Sonuçta bu şimdilik deneysel bir proje)
dotnet tool install -g Microsoft.Tye --version "0.5.0-alpha.20555.1"

# Kubernetes deployment öncesi Service Discovery için kullanacağımız local registry
docker run -d -p 5000:5000 --restart=always --name registry registry:2

# Docker Desktop tarafında Enable Kubernetes seçeneğinin de işaretli olması lazım
# Kubernetes'in etkin olduğunu anlamak içinse aşağıdaki komut işletilebilir
kubectl config current-context
# Bize docker-desktop cevabını vermeli
```

## Proje iskeleti

```bash
mkdir Starcups
cd Starcups
# Bir tane frontend uygulaması. Razor tipinde.
dotnet new razor -n HeadOffice
# frontend'in konuşacağı bir WebAPI
dotnet new webapi -n StockCollector
dotnet new sln
dotnet sln add HeadOffice StockCollector
```