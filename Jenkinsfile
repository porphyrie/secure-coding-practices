pipeline {
    agent any
    stages {
        stage('SonarQube Scan') {
            steps {
                // This step now uses the SonarQube server configuration from Jenkins system settings
                withSonarQubeEnv('sonar-server') { // Replace 'sonar-server' with the name you gave your SonarQube server configuration
                    // Scan Java directory
                    sh 'sonar-scanner -Dsonar.projectKey=secure-coding-practices -Dsonar.sources=./Java'
                    // Scan C directory
                    sh 'sonar-scanner -Dsonar.projectKey=secure-coding-practices -Dsonar.sources=./C'
                }
            }
        }
    }
}
