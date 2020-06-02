import React, {useEffect, useState} from 'react';
import {View, StyleSheet, Alert, ScrollView, Text, Image} from 'react-native';
import { Button } from 'react-native-elements';
import AsyncStorage from "@react-native-community/async-storage";
import config from "../../config/default.json";


export default function Products(props) {

    const [products, setProducts] = useState({
        requests: [],
        orderId: props.orderId,
    });


    function objectToQueryString(obj) {
        return Object.keys(obj).map(key => key + '=' + obj[key]).join('&');
    }

   // const [ customer,  setCustomer] = useState(null);


   /* const getUserInfo = async (userId) => {
        try{
            const token = await AsyncStorage.getItem('jwt');

            if(token) {

                const url = config.url + '/api/Customers/customer-info-by-user-id/'+ userId;

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
                    setCustomer(result);
                    return result;
                }
            }
        }catch (e) {
            console.log(e);
        }
    };
*/


    const approve = async (id) => {
        try{
            const token = await AsyncStorage.getItem('jwt');
            if(token) {
                const url = config.url + '/api/OrderRequest/approve-order-request/?' + objectToQueryString({requestId: id});
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
            }
        }catch (e) {
            console.log(e);
        }
    };


    const reject = async (id) => {
        try{
            const token = await AsyncStorage.getItem('jwt');
            if(token) {
                const url = config.url + '/api/OrderRequest/reject-order-request/?' + objectToQueryString({requestId: id});
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
            }
        }catch (e) {
            console.log(e);
        }
    };


    async function getRequests(){
        try{

            const token = await AsyncStorage.getItem('jwt');
            if(token) {
                const url = config.url + '/api/OrderRequest/get-order-request/' + products.orderId;

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

                    setProducts({...products, requests: result});

                }
            }

        }catch (e) {
            console.log(e);
        }
    }

    useEffect(() => {

        async function fetchData(){
            const data =  await getRequests()
        }
        fetchData();

    },[]);

        return (
            <ScrollView >
                <View style={styles.container}>

                    <View style={styles.buttonBlock}>
                        <View style={styles.button}>
                            <Button
                                type="solid"
                                title="Close"
                                linearGradientProps={{
                                    colors: ['pink', '#a62245' ],
                                    start: { x: 0, y: 0.5 },
                                    end: { x: 0, y: 0.5 },
                                }}
                                onPress={() => {
                                    props.getOrders(false);
                                }}
                            />
                        </View>
                    </View>
                    <View style={styles.logoBlock}>
                        <Text style={styles.logo}>Requests</Text>
                    </View>
                    {
                        products.requests.map((value, index) => {

                         //getUserInfo(value.delivery.userId);
                        if(value.approved === null){
                            return (
                                <View key={index} >
                                    <View style={styles.info}>
                                        <Text>rating: {value.delivery.rating}</Text>
                                    </View>
                                    <View style={styles.buttonContainer}>
                                        <View style={styles.button}>
                                            <Button
                                                type="solid"
                                                title="approve"
                                                linearGradientProps={{
                                                    colors: ['pink', '#0a7161' ],
                                                    start: { x: 0, y: 0.5 },
                                                    end: { x: 0, y: 0.5 },
                                                }}
                                                onPress={ async () => {
                                                    await approve(value.orderRequestId);
                                                    await getRequests();
                                                }}
                                            />
                                        </View>
                                        <View style={styles.button}>
                                            <Button
                                                type="solid"
                                                title="reject"
                                                linearGradientProps={{
                                                    colors: ['pink', '#a62245' ],
                                                    start: { x: 0, y: 0.5 },
                                                    end: { x: 0, y: 0.5 },
                                                }}
                                                onPress={ async () => {
                                                    await reject(value.orderRequestId);
                                                    await getRequests();
                                                }}
                                            />
                                        </View>
                                    </View>
                                </View>
                            )
                        }
                        })
                    }
                </View>
            </ScrollView>
        );
}


const styles = StyleSheet.create({

    container: {
        marginTop: 50,
        marginBottom: 50,
        alignItems: 'center',
        justifyContent: 'center'
    },
    orderBlock:{
        marginTop: 20,
        marginBottom: 20,
    },
    orderTitle:{
        fontWeight:'bold'
    },
    orderContent:{
        marginBottom:10
    },
    button:{
        marginTop: 20,
        marginBottom: 20,
        width: 290,
    },
    separator:{
        marginTop: 10,
    }
});