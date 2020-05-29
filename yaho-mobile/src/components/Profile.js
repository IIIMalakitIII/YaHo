import React from 'react';
import {Button, Text, View} from 'react-native';


export default function Profile({ navigation }) {
    return (
        <View style={{ flex: 1, alignItems: 'center', justifyContent: 'center' }}>
            <Text>Details Screen</Text>

            <Button title="Go to Home" onPress={() => navigation.navigate('Home')} />

        </View>
    );
}