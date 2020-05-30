import React from 'react';
import {StyleSheet, Text, View} from 'react-native';
import AsyncStorage from "@react-native-community/async-storage";

export default function Profile({ navigation }) {

    return (
        <View style={styles.container}>
            <Text style={styles.text}> Hello, </Text>
        </View>
    );
}



const styles = StyleSheet.create({
    container: {
        height:600,
        alignItems: 'center',
        flexDirection: 'column',
        justifyContent: 'center',
    },
    text:{

        fontSize: 25,
        textAlign: "center",
    }
});