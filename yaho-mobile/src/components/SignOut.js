import React, {useEffect} from 'react';
import {View, Button, Text, BackHandler} from 'react-native';
import AsyncStorage from "@react-native-community/async-storage";



export default function SignOut({route, navigation}) {

    const removeValue = async () => {
        try {
            await AsyncStorage.removeItem('jwt');
        } catch (e) {
            console.log(e);
        }
    };


    return(
        <View>
            <Text>Sure?</Text>
            <Button
                onPress={() =>
                {
                    const remove = removeValue();
                    route.params.setToken(null);
                }}
                title="Yes"
            />

        </View>
    )

}
