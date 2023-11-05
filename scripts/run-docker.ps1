# Define the image name and tag
$imageName = "ungerfall-currencies"
$tag = "latest"
$containerName = "ungerfall-currencies"
$port = "44406"

cd "${PSScriptRoot}\.."

# Build the Docker image
docker build -t ${imageName}:${tag} .

# Stop and remove any running container with the same name
docker stop ${containerName}
docker remove ${containerName} -f

# Run the Docker container
docker run -d --name ${containerName} -p ${port}:${port} ${imageName}:${tag}

