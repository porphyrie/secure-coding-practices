pipeline {
        agent none
        stages {
         
          stage("SonarQube Scanner") {
            agent any
            steps {
              withSonarQubeEnv('sonar-server') {
                sh 'mvn clean package sonar:sonar'
              }
            }
          }
        }
      }
