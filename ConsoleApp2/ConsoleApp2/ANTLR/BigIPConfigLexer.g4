lexer grammar BigIPConfigLexer;

RECORD_PRE: 'ltm' (' ' | '\t')+ ;
TYPE: 'node' | 'pool' | 'rule' | 'virtual' | 'monitor' ;
RECORD_POST: (' ' | '\t')+ '/' (~[{}\r\n/])*? '/' (~[{}\r\n])*? (' ' | '\t')+ ;
LBRACE: '{';
RBRACE: '}';
DASH: '-';
SLASH: '/';
ANY: . ;
WS: [ \t\r\n]+;  // No ignora espacios en blanco, tabulaciones, o saltos de línea
NEWLINE: '\r'? '\n'; // Añade una regla explícita para los saltos de línea