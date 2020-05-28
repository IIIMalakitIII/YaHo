import React from 'react';
import {View, StyleSheet, TextInput, Button, Alert} from 'react-native';
import config from '../../config/default'

export default function SignUpForm() {

    const [state, setState] = React.useState({
        id: null,
        firstName: '',
        lastName: '',
        phoneNumber: '',
        email: '',
        password: '',
        description: null
    });

    async function register(){


        try{
            const url = config.url + '/api/Account/sign-up';
            //console.log(state);

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


            if(response.ok){
                Alert.alert('Вы усешно зарегистрировались!');
            }else{
                const data = await response.json();
                Alert.alert(data.error || 'Что-то пошло не так');
            }

        }catch (e) {
            console.log(e);
        }

    }

    return (

        <View>
            <TextInput

                name = 'firstName'
                placeholder = 'first name'
                style={styles.form}
                onChangeText={text => setState({
                    ...state,
                    firstName: text
                })}
                value={state.firstName}
            />
            <TextInput
                name = 'lastName'
                placeholder = 'last name'
                style={styles.form}
                onChangeText={text => setState({
                    ...state,
                    lastName: text
                })}
                value={state.lastName}
            />
            <TextInput
                name = 'phoneNumber'
                placeholder = 'phone number'
                style={styles.form}
                onChangeText={text => setState({
                    ...state,
                    phoneNumber: text
                })}
                value={state.phoneNumber}
            />
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
                title='Sing Up'
                onPress={() => {
                    register();
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