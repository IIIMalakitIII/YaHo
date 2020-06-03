import React, {useEffect, useState} from 'react';
import {View, StyleSheet, Alert, ScrollView, Text, Image} from 'react-native';
import { Button } from 'react-native-elements';
import AsyncStorage from "@react-native-community/async-storage";
import config from "../../config/default.json";


export default function userRequests(props) {

    const [products, setProducts] = useState({
        requests: [],
        orderId: props.orderId,
    });

    const [orders, setOrders] = useState([]);

    function objectToQueryString(obj) {
        return Object.keys(obj).map(key => key + '=' + obj[key]).join('&');
    }


    async function getAllOrders(){

        let array = [];
        products.requests.map(x=>{
            if(array.indexOf(x.orderId) === -1){
                array.push(x.orderId)
            }
        })

        for(let i = 0 ; i< array.length; i++){
            await getOrder(array[i]);
        }

        setOrders(orders);

    }

    async function getOrder(id){
        try{

            const token = await AsyncStorage.getItem('jwt');
            if(token) {
                const url = config.url + '/api/Orders/order-by-id/' + id;

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
                    let array =  orders;
                    array.push(result);
                    setOrders(array);
                }
            }

        }catch (e) {
            console.log(e);
        }
    }


    async function deleteRequest(id){
        try{

            const token = await AsyncStorage.getItem('jwt');
            if(token) {
                const url = config.url + '/api/OrderRequest/delete-my-order-request/?' + objectToQueryString({requestId: id});

                const response = await fetch(url, {
                    method: 'DELETE',
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
                }
            }

        }catch (e) {
            console.log(e);
        }
    }

    useEffect(() => {

        async function fetchData(){

            const data =  await getRequests();
        }
        fetchData();


    },[]);


    async function getRequests(){
        try{

            const token = await AsyncStorage.getItem('jwt');
            if(token) {
                const url = config.url + '/api/OrderRequest/get-my-request';

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

    return (
        <ScrollView >
            <View style={styles.container}>

                <View style={styles.buttonBlock}>
                    <View style={styles.buttonClose}>
                        <Button
                            type="solid"
                            title="update"
                            linearGradientProps={{
                                colors: ['pink', '#0a7161' ],
                                start: { x: 0, y: 0.5 },
                                end: { x: 0, y: 0.5 },
                            }}
                            onPress={ async  () => {

                                await getRequests();
                                await getAllOrders();
                                await getRequests();
                            }}
                        />
                    </View>
                </View>

                <View style={styles.logoBlock}>
                    <Text style={styles.logo}>My requests</Text>
                </View>
                {
                    products.requests.map((value, index) => {


                        //getOrder(value.orderId);

                        let approved ;

                        if(value.approved === null ){
                            approved = 'in expectation';
                        }else if(value.approved === true ){
                            approved= 'approved';
                        }else if(value.approved === false ){
                            approved = 'canceled';
                        }

                        let order;

                        orders.map(x=>{
                            if(x.orderId === value.orderId){
                                order = x;
                            }
                        })

                        if(order !== undefined) {
                            return (
                                <View key={index}>
                                    <View style={styles.info}>
                                        <View style={styles.orderContent}>
                                            <Text style={styles.orderTitle}> {order.title}</Text>
                                            <Text> status: {approved}</Text>
                                            <Text> Delivery from: {order.deliveryFrom}</Text>
                                            <Text> Delivery to: {order.deliveryPlace}</Text>
                                            <Text> Initial date: {order.initialDate.split('T')[0]}</Text>
                                            <Text> Expected date: {order.expectedDate.split('T')[0]}</Text>
                                            <Text> Comment: </Text>
                                            <Text> {order.comment}</Text>
                                        </View>


                                    </View>
                                    <View style={styles.buttonContainer}>

                                        {
                                            value.approved !== true ?
                                                <View style={styles.button}>
                                                    <Button
                                                        type="solid"
                                                        title="delete"
                                                        linearGradientProps={{
                                                            colors: ['pink', '#a62245'],
                                                            start: {x: 0, y: 0.5},
                                                            end: {x: 0, y: 0.5},
                                                        }}
                                                        onPress={async () => {
                                                            await deleteRequest(value.orderRequestId);
                                                            await getRequests();
                                                        }}
                                                    />
                                                </View> : <View/>
                                        }
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