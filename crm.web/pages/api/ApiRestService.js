import axios from 'axios'
import Document from 'next/document'

export default class ApiRestService {

    constructor() {
        let headers = {
            'Content-Type': 'application/json'
        }

        this.http = axios.create({
            baseURL: 'https://localhost:44395/',
            timeout: 60000,
            headers: headers
        })
    }

    getInstance() {
        return this.http;
    }
}