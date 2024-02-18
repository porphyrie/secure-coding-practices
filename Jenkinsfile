pipeline {
    agent any
    stages {
        stage('Build Projects') {
            steps {
                script {
                    // Compile all Java projects except arbitraryfileupload with javac
                    sh '''
                    for dir in ./Java/*/src; do
                        if [[ "$dir" != *"arbitraryfileupload/src"* ]]; then
                            base=$(dirname "$dir")
                            mkdir -p "$base/bin"
                            javac -d "$base/bin" "$dir"/*.java
                        fi
                    done
                    '''
                }
            }
        }
        stage('SonarQube Scan') {
            steps {
                withSonarQubeEnv('sonar-server') {
                    script {
                        // Dynamically find all src directories for sonar.sources
                        def sources = sh(script: "find ./Java -type d -name src | tr '\\n' ','", returnStdout: true).trim()
                        // Remove the trailing comma
                        sources = sources[0..-2]
                        
                        // Dynamically find all bin directories for sonar.java.binaries, including arbitraryfileupload if it's built separately by Maven
                        def binaries = sh(script: "find ./Java -type d -name bin | tr '\\n' ','", returnStdout: true).trim()
                        // Add the Maven project binary directory manually
                        binaries = binaries + "./Java/arbitraryfileupload/target/classes,"
                        // Remove the trailing comma
                        binaries = binaries[0..-2]
                        
                        // Run sonar-scanner with dynamically set properties
                        sh """
                        sonar-scanner \
                        -Dsonar.projectKey=secure-coding-practices \
                        -Dsonar.sources=${sources} \
                        -Dsonar.java.binaries=${binaries}
                        """
                    }
                }
            }
        }
    }
}
