import React from 'react';
import {View, StyleSheet, Alert, ScrollView} from 'react-native';
import config from '../../config/default'
import { Button, Input } from 'react-native-elements';


export default function SignUpForm({ navigation }) {

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
                navigation.navigate('Sign In');
            }else{
                const data = await response.json();
                Alert.alert(data.error || 'Что-то пошло не так');
            }

        }catch (e) {
            console.log(e);
        }

    }

    return (
        <ScrollView >
        <View style={styles.main}>
            <View style={styles.container}/>
            <View style={styles.container}>

                <View style={styles.inputBlock}>

                    <Input
                        name = 'firstName'
                        placeholder = 'first name'
                        style={styles.form}
                        onChangeText={text => setState({
                            ...state,
                            firstName: text
                        })}
                        value={state.firstName}
                    />
                    <Input
                        name = 'lastName'
                        placeholder = 'last name'
                        style={styles.form}
                        onChangeText={text => setState({
                            ...state,
                            lastName: text
                        })}
                        value={state.lastName}
                    />
                    <Input
                        name = 'phoneNumber'
                        placeholder = 'phone number'
                        style={styles.form}
                        onChangeText={text => setState({
                            ...state,
                            phoneNumber: text
                        })}
                        value={state.phoneNumber}
                    />
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
                            title='Sing Up'
                            onPress={() => {
                                register();
                            }}
                        />
                    </View>

                    <View style={styles.button}>
                        <Button
                            type="outline"
                            title="Sign In"
                            onPress={() => navigation.navigate('Sign In')}
                        />
                    </View>
                </View>
            </View>
            <View style={styles.container}/>
        </View>
        </ScrollView>

    );
}


const styles = StyleSheet.create({
    main: {
        justifyContent: 'center',
        flexDirection: 'column'
    },
    container: {
        height:220,
        alignItems: 'center',
        flexDirection: 'column',
        justifyContent: 'center',
    },
    buttonBlock: {
        marginTop: 40,
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