import React, {useEffect} from 'react';
import {View, Text, BackHandler, StyleSheet} from 'react-native';
import AsyncStorage from "@react-native-community/async-storage";
import { Button} from 'react-native-elements';


export default function SignOut({route, navigation}) {

    const removeValue = async () => {
        try {
            await AsyncStorage.removeItem('jwt');
        } catch (e) {
            console.log(e);
        }
    };


    return(
        <View style={styles.container}>
            <Text style={styles.text}>A you sure?</Text>

            <View style={styles.buttonBlock}>
                <View style={styles.button}>
                    <Button
                        onPress={() =>
                        {
                            navigation.goBack();
                        }}
                        title="No"
                    />
                </View>

                <View style={styles.button}>
                    <Button
                        type="outline"
                        onPress={() =>
                        {
                            const remove = removeValue();
                            route.params.setToken(null);
                        }}
                        title="Yes"
                    />
                </View>
            </View>

        </View>
    )

}



const styles = StyleSheet.create({
    container: {
        height:600,
        alignItems: 'center',
        flexDirection: 'column',
        justifyContent: 'center',
    },
    buttonBlock: {
        marginTop: 25,
        flexDirection: 'row',
        justifyContent: 'center',
    },
    button:{
        width: 150,
        marginLeft: 10,
        marginRight: 10,
    },
    text:{
        fontSize: 25,
        textAlign: "center",
    }
});