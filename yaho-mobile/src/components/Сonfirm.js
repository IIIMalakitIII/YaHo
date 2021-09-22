import React, {useEffect, useState} from 'react';
import {View, StyleSheet, Alert, ScrollView, Text, Image} from 'react-native';
import { Button } from 'react-native-elements';
import AsyncStorage from "@react-native-community/async-storage";
import config from "../../config/default.json";


export default function Confirm(props) {

    const [products, setProducts] = useState({
        statuses: [],
        orderId: props.orderId,
    });


    const updateStatus = async (status) => {
        try{
            const token = await AsyncStorage.getItem('jwt');
            if(token) {

                const url = config.url + '/api/Confirms/confirm-change-order-status-like-customer';

                const response = await fetch(url, {
                    method: 'POST',
                    mode: 'cors',
                    cache: 'no-cache',
                    credentials: 'same-origin',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token}`
                    },
                    redirect: 'follow',
                    referrerPolicy: 'no-referrer',
                    body: JSON.stringify({
                        orderId: products.orderId,
                        newOrderStatus: status
                    })
                });

                if (response.ok) {
                    const result = await response.json();
                }
            }
        }catch (e) {
            console.log(e);
        }
    };


    async function getStatuses(){
        try{

            const token = await AsyncStorage.getItem('jwt');
            if(token) {
                const url = config.url + '/api/Confirms/confirms-order-status/' + products.orderId;

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

                    setProducts({...products, statuses: result});

                }
            }

        }catch (e) {
            console.log(e);
        }
    }

    useEffect(() => {

        async function fetchData(){
            const data =  await getStatuses(products.orderId)
        }
        fetchData();

    },[]);

    return (
        <ScrollView >
            <View style={styles.container}>

                <View style={styles.buttonBlock}>
                    <View style={styles.buttonClose}>
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
                    <Text style={styles.logo}>Confirmations</Text>
                </View>
                {
                    products.statuses.map((value, index) => {


                        let status;
                        if(
                            value.newStatus === "0" ||
                            value.newStatus === 0
                        ){
                            status = 'Canceled'
                        }else if(
                            value.newStatus === "4" ||
                            value.newStatus === 4 ||
                            value.newStatus == 'Canceled'
                        ){
                            status = 'Canceled'
                        }else if(
                            value.newStatus === "5" ||
                            value.newStatus === 5 ||
                            value.newStatus == 'Done'
                        ){
                            status = 'Done'
                        }


                        return (
                            <View key={index} >
                                <View style={styles.info}>
                                    <Text>previous status: {value.previousStatus}</Text>
                                    <Text>new status: {status}</Text>
                                </View>
                                <View style={styles.buttonContainer}>

                                    <View style={styles.button}>
                                        <Button
                                            type="solid"
                                            title="confirm"
                                            linearGradientProps={{
                                                colors: ['pink', '#0a7161' ],
                                                start: { x: 0, y: 0.5 },
                                                end: { x: 0, y: 0.5 },
                                            }}
                                            onPress={ async () => {

                                                await updateStatus(status);
                                                await getStatuses();
                                            }}
                                        />
                                    </View>
                                    <View style={styles.button}>
                                        <Button
                                            type="solid"
                                            title="cancel"
                                            linearGradientProps={{
                                                colors: ['pink', '#a62245' ],
                                                start: { x: 0, y: 0.5 },
                                                end: { x: 0, y: 0.5 },
                                            }}
                                            onPress={ async () => {
                                                await updateStatus(value.previousStatus);
                                                await getStatuses();
                                            }}
                                        />
                                    </View>
                                </View>
                            </View>
                        )

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
    buttonClose:{
        marginTop: 20,
        marginBottom: 20,
        width: 290,
    },
    separator:{
        marginTop: 10,
    },
    buttonContainer:{
        flexDirection: 'row',
        marginTop: 10,
        marginBottom: 20,
    },
    button:{
        width: 130,
        marginRight: 15,
        marginLeft: 15,
    },
    info:{
        marginTop: 10,
        marginLeft: 15,
    },
    logoBlock:{
        marginTop:30,
        marginBottom:30,
        justifyContent:'center',
        alignItems: 'center',
    },
    logo:{
        fontSize:30,
        fontWeight: 'bold',
    },
});