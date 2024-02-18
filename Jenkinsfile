pipeline {
    agent any
    stages {
        stage('Build CanonicalizePathNameAfter') {
            steps {
                script {
                    // Create the bin directory if it does not exist
                    sh 'mkdir -p ./Java/CanonicalizePathNameAfter/bin'
                    // Compile the Java files from src to bin directory
                    sh 'javac -d ./Java/CanonicalizePathNameAfter/bin ./Java/CanonicalizePathNameAfter/src/*.java'
                }
            }
        }
        stage('SonarQube Scan') {
            steps {
                withSonarQubeEnv('sonar-server') { // Ensure 'sonar-server' matches your SonarQube server configuration in Jenkins
                    // Scan for ./Java/arbitraryfileupload (Maven project)
                    sh '''
                    sonar-scanner \
                    -Dsonar.projectKey=secure-coding-practices \
                    -Dsonar.sources=./Java/arbitraryfileupload/src \
                    -Dsonar.java.binaries=./Java/arbitraryfileupload/target/classes
                    '''
                    // Scan for ./Java/CanonicalizePathNameAfter (Non-Maven, built with javac)
                    sh '''
                    sonar-scanner \
                    -Dsonar.projectKey=secure-coding-practices \
                    -Dsonar.sources=./Java/CanonicalizePathNameAfter/src \
                    -Dsonar.java.binaries=./Java/CanonicalizePathNameAfter/bin
                    '''
                }
            }
        }
    }
}
