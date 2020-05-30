import React from 'react';
import {View, StyleSheet, TextInput, Button, Alert} from 'react-native';
import config from '../../config/default'
import AsyncStorage from "@react-native-community/async-storage";




export default function SignInForm({ route, navigation}) {

    const [state, setState] = React.useState({
        email: 'mongo@gmail.com',
        password: 'mongo123456'
    });


    const storeData = async (value) => {
        try {
            await AsyncStorage.setItem('jwt', value);
        } catch (e) {
            console.log(e);
        }
    };

    async function  login(){

        try{
            const url = config.url + '/api/Account/sign-in';

            console.log(state);
            const response = await fetch(url, {
                method: 'POST',
                mode: 'cors',
                cache: 'no-cache',
                credentials: 'same-origin',
                headers: {
                    'Content-Type': 'application/json'
                },
                redirect: 'follow',
                referrerPolicy: 'no-referrer',
                body: JSON.stringify(state)
            });


            const result = await response.json();

            if(response.ok){

                await storeData(
                    result.token
                );
                route.params.setToken(result.token);

            }else{

                Alert.alert(data.error || 'Что-то пошло не так');
            }

        }catch (e) {

            console.log(e);
        }

    }

    return (

        <View>
            <TextInput
                name = 'email'
                placeholder = 'email'
                style={styles.form}
                onChangeText={text => setState({
                    ...state,
                    email: text
                })}
                value={state.email}
            />
            <TextInput
                name = 'password'
                placeholder = 'password'
                style={styles.form}
                onChangeText={text => setState({
                    ...state,
                    password: text
                })}
                value={state.password}
            />
            <Button
                color='#341bff'
                title='Sing In'
                onPress={() => {
                   login();
                }}
            />

            <Button
                title="Sign Up"
                onPress={() => navigation.navigate('Sign Up')}
            />

        </View>
    );
}




const styles = StyleSheet.create({
    form: {
        height: 40,
        borderColor: '#000',
        borderWidth: 1,
        width: 300
    }

});