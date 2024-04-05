export interface VirtualServer {
    id: number;
    name: string;
    pool?: PoolDetails;
    rules?: Rule[];
    showDetails: boolean;
    selectedMember?: Node;
    showPoolDetails: boolean;  // Agregar esta línea
    showMemberList: boolean;  // Agrega esta línea
    showRuleList: boolean; // Agrega esta línea
    showMonitorDetails: boolean;
    isExpanded: boolean,
    
  }
  
  
 export interface Pool {
    id: number;
    name: string;
    description?: string;
    balancerType?: string;
    monitor?: Monitor;
    nodePools?: NodePool[];
    showDetails: boolean;
    showMembers: boolean;
    virtuals: VirtualServer[];
    showVirtuals:boolean;
    showMonitor: boolean;
    selectedMember?: Node;
  }

  export interface NodePool {
    node: Node;
    nodePort: string;
    pool: Pool;
  }

  export interface Node {
    name: string;
    id: number;
    ip: string;
    description?: string;
    nodePools?: NodePool[];
    showDetails: boolean;
    isExpanded: boolean;
    showPools: boolean;
    
  }
  
  export interface PoolDetails extends Pool {
    description?: string;
    balancingType?: string;
    monitor?: Monitor;
  }

  
  
  
  export interface Rule {
    id: number;
    name: string;
    showDetails?: boolean;
    irules?: IRule[];
    virtual: VirtualServer;
    showIRules: boolean;
    showVirtualDetails: boolean,
    virtualId: string,
  }

  export interface IRule {
    name?: string;
    redirect?: string;
    showDetails: boolean;
  }
  
  
  
  export interface Monitor {
    id: number;
    name: string;
    adaptive?: string;
    cipherlist?: string;
    compatibility?: string;
    debug?: string;
    defaults_from?: string;
    description?: string;
    destination?: string;
    iP_DSCP?: string;
    interval?: string;
    password?: string;
    recv?: string;
    recV_disable?: string;
    reverse?: string;
    send?: string;
    server?: string;
    service?: string;
    get?: string;
    ssl_profile?: string;
    time_until_up?: string;
    timeout?: string;
    username?: string;
    showDetails: boolean;
    showPools: boolean,
    pools?: Pool[],
    showMemberList: boolean;
    selectedMember?: Node;
    showGeneralDetails: boolean;
    // Otras propiedades del monitor
  }