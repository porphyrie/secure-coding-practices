pipeline {
    agent any
    stages {
        stage('Prepare SonarQube Sources') {
            steps {
                script {
                    // Dynamically find and set the SONAR_SOURCES using a Bash command
                    env.SONAR_SOURCES = sh(
                        script: "find Java -type d -name src | grep -v 'Java/arbitraryfileupload/src' | tr '\\n' ',' | sed 's/,\$//'",
                        returnStdout: true
                    ).trim()
                    // Print the dynamically generated SONAR_SOURCES for verification
                    echo "SONAR_SOURCES: ${env.SONAR_SOURCES}"
                }
            }
        }
        stage('SonarQube Scan') {
            steps {
                // This step now uses the SonarQube server configuration from Jenkins system settings
                withSonarQubeEnv('sonar-server') {
                    // Scan directories specified in the SONAR_SOURCES environment variable
                    sh "sonar-scanner -Dsonar.projectKey=secure-coding-practices -Dsonar.sources=${env.SONAR_SOURCES}"
                }
            }
        }
    }
}
