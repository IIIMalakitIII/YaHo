import React , { useState, useEffect }from 'react';
import {StyleSheet, Text, View, ScrollView, SafeAreaView} from 'react-native';
import AsyncStorage from "@react-native-community/async-storage";
import config from "../../config/default";
import { Button, Input} from 'react-native-elements';
import Products from "./Products";
import AddOrder from "./AddOrder";
import OrderRequests from "./OrderRequests";
import OrderDetails from "./OrderDetails";


export default function UserDeliveries({ navigation }) {

    const [orders, setOrders] = useState([]);


    const updateStatus = async (status, id) => {
        try{
            const token = await AsyncStorage.getItem('jwt');
            if(token) {

                const url = config.url + '/api/Confirms/confirm-change-order-status-like-delivery';

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
                        orderId: id,
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


    //need
    const getDeliveries = async (flag) => {
        try{
            setProducts({ ...products, create:  flag, requests: flag})
            const token = await AsyncStorage.getItem('jwt');

            if(token) {

                const url = config.url + '/api/Orders/my-order-like-delivery';

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
            const data =  await getDeliveries();
        }
        fetchData();

    },[]);


    const [products, setProducts] = useState({
        open: false,
        products: [],
        orderId: 0
    });


   if(products.open){

        return (
            <OrderDetails products = {products} setProducts = {setProducts} delivery = {true} />
        );

    }else{

        return (

            <ScrollView >
                <View style={styles.container}>
                    <View style={styles.logoBlock}>
                        <Text style={styles.logo}>Deliveries</Text>
                    </View>

                    {
                        orders.map((value, index) => {


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


                                    <Button
                                        linearGradientProps={{
                                            colors: ['pink', '#0a7161'],
                                            start: {x: 0, y: 0.5},
                                            end: {x: 0, y: 0.5},
                                        }}
                                        title="I`ve done it"
                                        onPress={ async () => {
                                            await updateStatus('Done', value.orderId);
                                            await getDeliveries();
                                        }}
                                    />


                                    <View style = {styles.separator}/>


                                    <Button
                                        linearGradientProps={{
                                            colors: ['pink', '#a62245'],
                                            start: {x: 0, y: 0.5},
                                            end: {x: 0, y: 0.5},
                                        }}
                                        title= 'cancel'
                                        onPress={ async () => {
                                            await updateStatus('Canceled', value.orderId);
                                            await getDeliveries();
                                        }}
                                    />
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