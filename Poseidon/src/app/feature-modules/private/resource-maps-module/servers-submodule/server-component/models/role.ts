export class Role {
    id !: string;
    name !: string;
    type !: number;
    serverId !: string;
    dateInsert!: Date;
    dateModify!: Date;
    dateDisable!: Date;
    active!: boolean;
}