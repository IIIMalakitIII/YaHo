import React , { useState, useEffect }from 'react';
import {Alert, StyleSheet, Text, View} from 'react-native';
import AsyncStorage from "@react-native-community/async-storage";
import config from "../../config/default";

export default function Profile({ route, navigation }) {

    const [user, setUser] = useState({

        email: '',
        firstName:'' ,
        lastName: '',
        phoneNumber: '',
        description: ''
    });


    const getInfo = async () => {
        try{

            const token = await AsyncStorage.getItem('jwt');

            if(token) {

                const url = config.url + '/api/Customers/my-customer-info';

                const response = await fetch(url, {
                    method: 'GET',
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

                    const result = await response.json();
                    setUser(result.userInfo);
                    return result;
                }
            }
        }catch (e) {
            console.log(e);
        }
    };


    useEffect(() => {

        async function fetchData(){
            const data =  await getInfo();
        }
       fetchData();

    },[]);



    return (
        <View style={styles.container}>
            <Text style={styles.text}>Hello, {user.firstName} {user.lastName}</Text>
            <Text style={styles.text}>email: {user.email}</Text>
            <Text style={styles.text}>mobile: {user.phoneNumber}</Text>
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