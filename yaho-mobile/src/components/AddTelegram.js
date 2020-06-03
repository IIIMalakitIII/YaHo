import React , { useState, useEffect }from 'react';
import {Alert, StyleSheet, Text, View} from 'react-native';
import AsyncStorage from "@react-native-community/async-storage";
import config from "../../config/default";
import {Button, Input} from "react-native-elements";

export default function AddTelegram({ navigation }) {

    const [state, setState] = useState('');


    function objectToQueryString(obj) {
        return Object.keys(obj).map(key => key + '=' + obj[key]).join('&');
    }

    const addAccount = async () => {
        try{

            const token = await AsyncStorage.getItem('jwt');

            if(token) {
                const url = config.url + '/api/Account/update-user-telegramId/?' + objectToQueryString({telegramId: state});
                const response = await fetch(url, {
                    method: 'PUT',
                    mode: 'cors',
                    cache: 'no-cache',
                    credentials: 'same-origin',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token}`
                    },
                    redirect: 'follow',
                    referrerPolicy: 'no-referrer',
                });

                if (response.ok) {
                    Alert.alert('Successfully!');
                }else{
                    Alert.alert('error!');
                    setState('');
                }


            }
        }catch (e) {
            console.log(e);
        }
    };


    return (
        <View style={styles.container}>
            <Text style={styles.logo}>Send Your Telegram ID</Text>
            <View style={styles.inputBlock}>
                <Input
                    name = 'id'
                    placeholder = 'id'
                    style={styles.form}
                    onChangeText={text => setState(text)}
                    value={state}
                />
            </View>
            <View style={styles.button}>

                <Button
                    linearGradientProps={{
                        colors: ['pink', '#125fb3'],
                        start: {x: 0, y: 0.5},
                        end: {x: 0, y: 0.5},
                    }}
                    title= {`Add account`}
                    onPress={ async () => {
                       await addAccount();
                    }}
                />
            </View>
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
    },
    inputBlock: {

        width:270,
        flexDirection: 'column',
        justifyContent: 'center',
    },
    button:{
        width: 250,
        marginLeft: 10,
        marginRight: 10
    },
    logo:{
        fontSize:20,
        fontWeight: 'bold',
        marginBottom: 20
    }
});