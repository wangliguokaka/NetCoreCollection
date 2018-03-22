cd NetCorePublish
"D:\Program Files (x86)\WinRAR\winrar" x source.zip
cd JenkinsPublishBuild
docker build -t wlgneturl/autowebfile001 .
docker stop containerfile 
docker rm containerfile 
docker run --name containerfile -d -p 5001:5001 wlgneturl/autowebfile001
echo ..