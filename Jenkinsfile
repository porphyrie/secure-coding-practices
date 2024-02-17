pipeline {
    agent any
    stages {
        stage('Prepare SonarQube Sources') {
            steps {
                script {
                    // Use a Bash command to find all 'src' directories under 'Java'
                    // and concatenate them into a single string separated by commas
                    env.SONAR_SOURCES = sh(
                        script: "find Java -type d -name src | tr '\\n' ',' | sed 's/,\$//'",
                        returnStdout: true
                    ).trim()
                }
            }
        }
        stage('SonarQube Scan') {
            steps {
                // This step now uses the SonarQube server configuration from Jenkins system settings
                withSonarQubeEnv('sonar-server') { // Replace 'sonar-server' with the name you gave your SonarQube server configuration
                    // Scan directories specified in the SONAR_SOURCES environment variable
                    sh "sonar-scanner -Dsonar.projectKey=secure-coding-practices -Dsonar.sources=${env.SONAR_SOURCES}"
                }
            }
        }
    }
}
