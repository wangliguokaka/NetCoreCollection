# ����microsoft/dotnet:latest����Docker Image
FROM microsoft/dotnet:latest
 
# ����docker�е�/usr/local/srcĿ¼
RUN cd /usr/local/src
 
# ����DockerWebAPIĿ¼
RUN mkdir DockerWebAPIFile
 
# ���ù���·��
WORKDIR /usr/local/src/DockerWebAPIFile
 
# ����ǰ�ļ����µ������ļ�ȫ�����Ƶ�����Ŀ¼
COPY . /usr/local/src/DockerWebAPIFile
 
# ����籩¶5000�˿�
EXPOSE 5000
 
# ִ��dotnet WebAPI.dll����
CMD ["dotnet", "WebGeestar.dll"]