for this we had to work with Docker and download jaeger image for tracing

docker run -d --name jaeger -p 16686:16686 -p 6831:6831/udp jaegertracing/all-in-one:1.21
