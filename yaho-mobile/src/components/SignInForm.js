import React from 'react';
import {View, StyleSheet, Alert} from 'react-native';
import config from '../../config/default'
import AsyncStorage from "@react-native-community/async-storage";
import { Button, Input } from 'react-native-elements';



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

                Alert.alert('Что-то пошло не так');
            }

        }catch (e) {

            console.log(e);
        }

    }

    return (


        <View style={styles.main}>
        <View style={styles.container}/>
        <View style={styles.container}>
            <View style={styles.inputBlock}>

                <Input
                    name = 'email'
                    placeholder = 'email'
                    style={styles.form}
                    onChangeText={text => setState({
                        ...state,
                        email: text
                    })}
                    value={state.email}
                />
                <Input
                    name = 'password'
                    placeholder = 'password'
                    style={styles.form}
                    onChangeText={text => setState({
                        ...state,
                        password: text
                    })}
                    value={state.password}
                />

            </View>

            <View style={styles.buttonBlock}>

                <View style={styles.button}>
                    <Button
                        type="solid"
                        title='Sing In'
                        onPress={() => {
                           login();
                        }}
                    />
                </View>
                <View style={styles.button}>

                    <Button
                        type="outline"
                        title="Sign Up"
                        onPress={() => navigation.navigate('Sign Up')}
                    />
                </View>
            </View>

        </View>
    <View style={styles.container}/>
    </View>
    );
}


const styles = StyleSheet.create({
    main: {
        justifyContent: 'center',
        flexDirection: 'column'
    },
    container: {
        height: 250,
        alignItems: 'center',
        flexDirection: 'column',
        justifyContent: 'center',
    },
    buttonBlock: {
        marginTop: 10,
        flexDirection: 'row',
        justifyContent: 'center',
    },
    inputBlock: {

        width:340,
        flexDirection: 'column',
        justifyContent: 'center',
    },
    button:{
        width: 150,
        marginLeft: 10,
        marginRight: 10
    },
});