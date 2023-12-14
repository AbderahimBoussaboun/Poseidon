grammar BigIPConfig;

config: (record | UNRECOGNIZED_LINE)* EOF;

record: LTM type SLASH 'Common' SLASH ID LCURL (lineContent | RCURL);

type: 'virtual' | 'node' | 'pool' | 'rule';

UNRECOGNIZED_LINE: ~['l''\n']* ('\n'| EOF) -> skip;

lineContent : (RCURL | TEXT lineContent);

LTM: 'ltm';
ID: [a-zA-Z0-9_]+;
TEXT: (~[{}'\n'])*;
SLASH: '/';
RCURL: '}';
LCURL: '{';
WS: [ \t] -> skip;
NEWLINE: '\r'? '\n';