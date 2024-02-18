pipeline {
    agent any
    stages {
        stage('SonarQube Scan') {
            steps {
                withSonarQubeEnv('sonar-server') { // Ensure 'sonar-server' matches your SonarQube server configuration in Jenkins
                    // Scan the specified Java directory
                    sh '''
                    sonar-scanner \
                    -Dsonar.projectKey=secure-coding-practices \
                    -Dsonar.sources=./Java/arbitraryfileupload/src \
                    -Dsonar.java.binaries=./Java/arbitraryfileupload/target/classes
                    '''
                }
            }
        }
    }
}
