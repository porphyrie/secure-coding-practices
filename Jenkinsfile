pipeline {
    agent any
    stages {
        stage('Build Projects') {
            steps {
                script {
                    sh '''
                    /bin/bash -c '\
                    for dir in ./Java/*/src; do
                        if [[ $dir != *"/arbitraryfileupload/src"* ]]; then
                            base=$(dirname "$dir")
                            mkdir -p "$base/bin"
                            javac -d "$base/bin" "$dir"/*.java
                        fi
                    done
                    
                    for csproj in $(find ./C# -name "*.csproj"); do
                        dotnet build "$csproj" -o $(dirname "$csproj")/bin
                    done
                    '
                    '''
                }
            }
        }
        stage('SonarQube Scan') {
            steps {
                withSonarQubeEnv('sonar-server') {
                    script {
                        // Dynamically find all src directories for Java and C#, and all bin directories
                        def javaSources = sh(script: "find ./Java -type d -name src | tr '\\n' ','", returnStdout: true).trim()
                        def csharpSources = sh(script: "find ./C# -type d -name src | grep -v bin | tr '\\n' ','", returnStdout: true).trim()
                        def javaBinaries = sh(script: "find ./Java -type d -name bin -o -path '*/target/classes' | tr '\\n' ','", returnStdout: true).trim()
                        def csharpBinaries = sh(script: "find ./C# -type d -name bin | tr '\\n' ','", returnStdout: true).trim()
                        
                        // Remove trailing commas
                        javaSources = javaSources[0..-2]
                        csharpSources = csharpSources[0..-2]
                        javaBinaries = javaBinaries[0..-2]
                        csharpBinaries = csharpBinaries[0..-2]

                        // Combine Java and C# sources and binaries
                        def sources = javaSources + "," + csharpSources
                        def binaries = javaBinaries + "," + csharpBinaries

                        // Generate sonar-project.properties file
                        writeFile file: 'sonar-project.properties', text: """
                        sonar.projectKey=secure-coding-practices
                        sonar.sources=${sources}
                        sonar.java.binaries=${javaBinaries}
                        sonar.cs.dotnet.binaries=${csharpBinaries}
                        """

                        // Execute sonar-scanner using the properties file
                        sh "sonar-scanner -Dproject.settings=sonar-project.properties"
                    }
                }
            }
        }
    }
}
