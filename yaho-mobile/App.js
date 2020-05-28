import React from 'react';
import { StyleSheet, Text, View, Button, Alert } from 'react-native';
import AuthPage from './src/components/AuthPage';



export default function App() {
  return (
      <View style={styles.container}>

          <AuthPage/>

      </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',

  }
});