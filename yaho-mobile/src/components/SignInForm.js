import React from 'react';
import {View, StyleSheet, TextInput, Button, Alert, AsyncStorage} from 'react-native';
import config from '../../config/default'



export default function SignInForm() {

    const [state, setState] = React.useState({
        email: '',
        password: ''
    });


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

                await AsyncStorage.setItem(
                    'jwt',
                    result.token
                );

                Alert.alert('Добро пожаловать!');

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