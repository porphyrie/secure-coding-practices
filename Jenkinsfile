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
                        // Dynamically find all src directories for Java
                        def javaSources = sh(script: "find ./Java -type d -name src | tr '\\n' ','", returnStdout: true).trim()
                        // Remove the trailing comma
                        javaSources = javaSources[0..-2]

                        // Dynamically find all src directories for C#
                        def csharpSources = sh(script: "find ./C# -type d -name '*' | grep -v bin | tr '\\n' ','", returnStdout: true).trim()
                        // Remove the trailing comma
                        csharpSources = csharpSources[0..-2]

                        // Combine Java and C# sources
                        def sources = javaSources + "," + csharpSources

                        // Dynamically find all bin directories for Java
                        def javaBinaries = sh(script: "find ./Java -type d -name bin -o -path '*/target/classes' | tr '\\n' ','", returnStdout: true).trim()
                        // Remove the trailing comma
                        javaBinaries = javaBinaries[0..-2]

                        // Dynamically find all bin directories for C#
                        def csharpBinaries = sh(script: "find ./C# -type d -name bin | tr '\\n' ','", returnStdout: true).trim()
                        // Remove the trailing comma
                        csharpBinaries = csharpBinaries[0..-2]

                        // Execute sonar-scanner with dynamically generated properties for both Java and C#
                        sh """
                        sonar-scanner \
                        -Dsonar.projectKey=secure-coding-practices \
                        -Dsonar.sources=${sources} \
                        -Dsonar.java.binaries=${javaBinaries} \
                        -Dsonar.cs.binaries=${csharpBinaries}
                        """
                    }
                }
            }
        }
    }
}
