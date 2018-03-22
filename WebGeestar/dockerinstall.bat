cd NetCorePublish\JenkinsPublishBuild
docker build -t wlgneturl/autowebfile001 autowebfile001 .
docker stop containerfile 
docker rm containerfile 
docker run --name containerfile -d -p 5001:5001 wlgneturl/autowebfile001
echo ..