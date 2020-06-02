import React , { useState, useEffect }from 'react';
import {StyleSheet, Text, View, ScrollView, SafeAreaView} from 'react-native';
import AsyncStorage from "@react-native-community/async-storage";
import config from "../../config/default";
import { Button, Input} from 'react-native-elements';
import Products from "./Products";
import AddOrder from "./AddOrder";
import OrderRequests from "./OrderRequests";


export default function UserOrders({ navigation }) {

    const [orders, setOrders] = useState([]);


    const updateStatus = async (status, id) => {
        try{
            const token = await AsyncStorage.getItem('jwt');
            if(token) {


                const url = config.url + '/api/Orders/update-order-status';

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
                    body: JSON.stringify({
                        orderId: id,
                        orderStatus: status
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


    const getOrders = async (flag) => {
        try{
            setProducts({ ...products, create:  flag, requests: flag})
            const token = await AsyncStorage.getItem('jwt');

            if(token) {

                const url = config.url + '/api/Orders/my-order-like-customer';

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
                    console.log("load orders");
                    setOrders(result);
                    return result;
                }
            }
        }catch (e) {
            console.log(e);
        }
    };


    useEffect(() => {

        async function fetchData(){
            const data =  await getOrders();
        }
        fetchData();

    },[]);


    const [products, setProducts] = useState({
        open: false,
        create: false,
        requests: false,
        products: [],
        orderId: 0
    });


    if(products.requests){

        return (
            <OrderRequests getOrders = {getOrders} orderId = {products.orderId} />
        );


    }else if(products.create){

        return (
            <AddOrder getOrders = {getOrders} />
        );

    } else if(products.open){


        return (
           <Products products = {products} setProducts = {setProducts} />
        );

    }else{

        return (

            <ScrollView >
                <View style={styles.container}>

                    <View style={styles.button}>
                        <Button
                            type="solid"
                            title="Create order"
                            onPress={() => {
                                setProducts({
                                    ...products,
                                    create: true
                                })
                            }}
                        />
                    </View>

                    {
                        orders.map((value, index) => {



                                let requestCount = 0;

                                value.orderRequests.map(x=>{
                                    if(x.approved === null ){
                                        requestCount +=1;
                                    }
                                })

                                let status;

                                if(value.orderStatus === 'InExpectation' ){
                                    status = 'in expectation';
                                }else if (value.orderStatus === 'InProcess'){
                                    status = 'in process';
                                }else if (value.orderStatus === 'Creating'){
                                    status = 'creating';
                                }else if (value.orderStatus === 'Canceled'){
                                    status = 'canceled';
                                }else if (value.orderStatus === 'Done'){
                                    status = 'done';
                                }

                                return (
                                    <View key={index} style={styles.orderBlock}>
                                        <View style={styles.orderContent}>
                                            <Text style={styles.orderTitle}> {value.title}</Text>
                                            <Text> {value.comment}</Text>
                                            <Text> Delivery from: {value.deliveryFrom}</Text>
                                            <Text> Delivery to: {value.deliveryPlace}</Text>
                                            <Text> Initial date: {value.initialDate.split('T')[0]}</Text>
                                            <Text> Expected date: {value.expectedDate.split('T')[0]}</Text>
                                            <Text> Status: {status}</Text>
                                        </View>
                                        <Button
                                            type="outline"
                                            title="overview"
                                            onPress={() => {
                                                setProducts({
                                                    open: true,
                                                    products: value.products,
                                                    orderId: value.orderId
                                                })
                                            }}
                                        />
                                    <View style = {styles.separator}/>

                                        {
                                            value.orderStatus === 'Creating' ?
                                            <Button

                                                linearGradientProps={{
                                                    colors: ['pink', '#0a7161'],
                                                    start: {x: 0, y: 0.5},
                                                    end: {x: 0, y: 0.5},
                                                }}
                                                title="publish"
                                                onPress={ async () => {
                                                    await updateStatus('InExpectation', value.orderId);
                                                    await getOrders();
                                                }}
                                            /> : <View/>
                                        }

                                        <View style = {styles.separator}/>
                                        {
                                            requestCount !== 0 ?
                                                <Button

                                                    linearGradientProps={{
                                                        colors: ['pink', '#a62245'],
                                                        start: {x: 0, y: 0.5},
                                                        end: {x: 0, y: 0.5},
                                                    }}
                                                    title= {`${requestCount} new requests`}
                                                    onPress={ async () => {
                                                        setProducts({
                                                            ...products,
                                                            requests: true,
                                                            orderId: value.orderId
                                                        })
                                                    }}
                                                /> : <View/>
                                        }

                                    </View>
                                )

                        })
                    }
                </View>
            </ScrollView>
        );
    }
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