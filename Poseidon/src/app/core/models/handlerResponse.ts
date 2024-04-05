export class HandlerResponse {

    getStatusCode(response: any): number {
        return response['StatusCode'];
    }

    getSuccess(response: any): boolean {
        return response['Success'];
    }

    getMessage(response: any): string {
        return response['Message'];
    }

    getData(response: any): any {
        return response['Data'];
    }
}