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
        stage('Compile') {
            steps {
                // Compile each Java source file found in the SONAR_SOURCES directories
                script {
                    env.SONAR_SOURCES.split(',').each { directory ->
                        def outputDir = "${directory}/../bin" // Set the output directory to be one level up from src
                        sh "mkdir -p ${outputDir}" // Create the bin directory if it doesn't exist
                        def compileCommand = "javac -d ${outputDir} ${directory}/*.java"
                        def compileStatus = sh(script: compileCommand, returnStatus: true)
                        if (compileStatus != 0) {
                            error "Compilation failed for directory: ${directory}"
                        }
                    }
                }
            }
        }
        stage('SonarQube Scan') {
            steps {
                withSonarQubeEnv('sonar-server') {
                    script {
                        // Scan directories specified in the SONAR_SOURCES environment variable
                        sh "find Java -path '*/bin/*.class' > sonar-java-binaries.txt"
                        // Read the contents of sonar-java-binaries.txt and assign it to sonarJavaBinaries variable
                        def sonarJavaBinaries = sh(script: 'cat sonar-java-binaries.txt', returnStdout: true).trim().replaceAll('\\n', ',')
                        // Print sonarJavaBinaries for verification
                        echo "Sonar Java Binaries: ${sonarJavaBinaries}"
                        // Pass sonarJavaBinaries to sonar scanner
                        sh "sonar-scanner -X -Dsonar.projectKey=secure-coding-practices -Dsonar.sources=${env.SONAR_SOURCES} -Dsonar.java.binaries=${sonarJavaBinaries}"
                    }
                }
            }
        }
    }
}
