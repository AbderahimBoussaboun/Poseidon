export class Server {
    id !: string;
    name !: string;
    ip !: string;
    location !: string;
    os !: string;
    environmentId !: string;
    infrastructureId !: string;
    productId !: string;
    dateInsert!: Date;
    dateModify!: Date;
    dateDisable!: Date;
    active!: boolean;
}