export class ComponentModel {
    id !: string;
    name !: string;
    ip !: string;
    ports !: string[];
    queryString !: string;
    subapplicationId !: string;
    balancerId !: string | null;
    componentTypeId !: string;
    dateInsert!: Date;
    dateModify!: Date;
    dateDisable!: Date;
    active!: boolean;
}