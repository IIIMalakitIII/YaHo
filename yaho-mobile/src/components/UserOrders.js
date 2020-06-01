import React , { useState, useEffect }from 'react';
import {StyleSheet, Text, View, ScrollView, SafeAreaView} from 'react-native';
import AsyncStorage from "@react-native-community/async-storage";
import config from "../../config/default";
import { Button, Input} from 'react-native-elements';
import Products from "./Products";
import AddOrder from "./AddOrder";


export default function Profile({ navigation }) {

    const [orders, setOrders] = useState([]);


    const getOrders = async (flag) => {
        try{
            setProducts({ ...products, create:  flag})
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
        products: [],
        orderId: 0
    });


    if(products.create){

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


                                return (
                                    <View key={index} style={styles.orderBlock}>
                                        <View style={styles.orderContent}>
                                            <Text style={styles.orderTitle}> {value.title}</Text>
                                            <Text> {value.comment}</Text>
                                            <Text> Delivery from: {value.deliveryFrom}</Text>
                                            <Text> Delivery to: {value.deliveryPlace}</Text>
                                            <Text> Initial date: {value.initialDate.split('T')[0]}</Text>
                                            <Text> Expected date: {value.expectedDate.split('T')[0]}</Text>
                                            <Text> Fault date: {value.expectedDateFault.split('T')[0]}</Text>
                                        </View>
                                        <Button
                                            type="outline"
                                            title="Overview"
                                            onPress={() => {
                                                setProducts({
                                                    open: true,
                                                    products: value.products,
                                                    orderId: value.orderId
                                                })
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
    }
});