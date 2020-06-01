import React, {useEffect, useState} from 'react';
import {View, StyleSheet, Alert, ScrollView, Text} from 'react-native';
import { Button } from 'react-native-elements';
import CreateProductForm from "./CreateProductForm";
import {Link} from "@react-navigation/native";
import AsyncStorage from "@react-native-community/async-storage";
import config from "../../config/default.json";




export default function Products(props) {

    const [products, setProducts] = useState({
        open: false,
        products: [],
        orderId: props.products.orderId
    });

    function objectToQueryString(obj) {
        return Object.keys(obj).map(key => key + '=' + obj[key]).join('&');
    }

    async function  getProducts(){

        const token = await AsyncStorage.getItem('jwt');

        try{
            const url = config.url + '/api/Products/products-by-order-id/?'+ objectToQueryString({orderId: products.orderId});


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

            if(response.ok){

                const result = await response.json();
                setProducts({...products, products: result});

            }

        }catch (e) {
            console.log(e);
        }
    }


    useEffect(() => {

        async function fetchData(){
            const data =  await getProducts()
        }
        fetchData();

    },[]);



    if(products.opened) {
        return (
            <CreateProductForm products = {products} setProducts = {setProducts}  />
        );
    }else{
        return (
            <ScrollView >
                <View style={styles.container}>

                    <View style={styles.buttonBlock}>
                        <View style={styles.button}>
                            <Button
                                type="solid"
                                title="Close"
                                onPress={() => {
                                    props.setProducts({
                                        open: false,
                                        products:[],
                                        orderId: 0
                                    })
                                }}
                            />
                        </View>

                        <View style={styles.button}>
                            <Button
                                type="solid"
                                title="Add Product"
                                onPress={() => {
                                    setProducts({
                                        ...products,
                                        open: true,
                                    })
                                }}
                            />
                        </View>
                    </View>
                    <View style={styles.logoBlock}>
                        <Text style={styles.logo}>Products</Text>
                    </View>
                    {
                        products.products.map((value, index) => {
                            if(products.products.length === 0){
                                return (
                                    <View key={index} style={styles.orderBlock}>
                                        <Text style={styles.orderTitle}>This order hasn`t any products</Text>
                                    </View>
                                )
                            }else{
                                return (
                                    <View key={index} >

                                        <View style={styles.orderBlock}>
                                            <Text style={styles.orderTitle}>{value.productName}</Text>
                                            <Link to={value.link} style={styles.link}>{value.link}</Link>
                                            <Text>Description: {value.description}</Text>
                                            <Text>Price: {value.price}</Text>
                                            <Text>Tax: {value.tax}</Text>
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
}


const styles = StyleSheet.create({

    container: {
        marginTop: 50,
        marginBottom: 50,
        alignItems: 'center',
        justifyContent: 'center'
    },

    orderBlock:{
        marginTop: 30,
        marginBottom: 20,
    },

    orderTitle:{
        fontWeight:'bold'
    },
    link:{
        color:'#1f89dd'
    },
    buttonBlock: {
        marginTop: 40,
        flexDirection: 'row',
        justifyContent: 'center',
    },
    button:{
        width: 150,
        marginLeft: 10,
        marginRight: 10
    },
    logoBlock:{
        marginTop:30,
        justifyContent:'center',
        alignItems: 'center',
    },
    logo:{
        fontSize:30,
        fontWeight: 'bold',
    }
});