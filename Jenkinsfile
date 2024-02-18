pipeline {
    agent any
    environment {
        // Define SEMGREP_APP_TOKEN for Semgrep Cloud authentication
        SEMGREP_APP_TOKEN = credentials('0c4017e6c28391fa1660f069647a6c28b700230ead5129c7264fbfa5e2e943a2')
    }
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
                    '
                    '''
                }
            }
        }
        // Add a Semgrep Scan stage
        stage('Semgrep Scan') {
            steps {
                script {
                    // Install Semgrep if not already present
                    sh 'pip3 install semgrep'
                    // Run Semgrep using the SEMGREP_APP_TOKEN for authentication with Semgrep Cloud
                    sh 'semgrep ci'
                }
            }
        }
        stage('SonarQube Scan') {
            steps {
                withSonarQubeEnv('sonar-server') {
                    script {
                        // Dynamically find all src directories for sonar.sources
                        def sources = sh(script: "find ./Java -type d -name src | tr '\\n' ','", returnStdout: true).trim()
                        sources = sources[0..-2] // Remove the trailing comma

                        // Dynamically find all bin directories for sonar.java.binaries, including Maven's target/classes
                        def binaries = sh(script: "find ./Java -type d -name bin -o -path '*/target/classes' | tr '\\n' ','", returnStdout: true).trim()
                        binaries = binaries[0..-2] // Remove the trailing comma
                        
                        // Execute sonar-scanner with dynamically generated properties
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
