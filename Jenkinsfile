pipeline {
    agent any
    stages {
        stage('Build Projects') {
            steps {
                script {
                    // Create the bin directory for CanonicalizePathNameAfter if it does not exist
                    sh 'mkdir -p ./Java/CanonicalizePathNameAfter/bin'
                    // Compile the Java files from src to bin directory for CanonicalizePathNameAfter
                    sh 'javac -d ./Java/CanonicalizePathNameAfter/bin ./Java/CanonicalizePathNameAfter/src/*.java'
                }
            }
        }
        stage('SonarQube Scan') {
            steps {
                withSonarQubeEnv('sonar-server') { // Ensure 'sonar-server' matches your SonarQube server configuration in Jenkins
                    // Single Scan combining both projects
                    sh '''
                    sonar-scanner \
                    -Dsonar.projectKey=secure-coding-practices \
                    -Dsonar.sources=./Java/arbitraryfileupload/src,./Java/CanonicalizePathNameAfter/src \
                    -Dsonar.java.binaries=./Java/arbitraryfileupload/target/classes,./Java/CanonicalizePathNameAfter/bin
                    '''
                }
            }
        }
    }
}
