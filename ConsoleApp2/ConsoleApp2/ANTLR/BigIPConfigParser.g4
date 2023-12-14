parser grammar BigIPConfigParser;

options { tokenVocab=BigIPConfigLexer; }

config : (record | notRecord)* EOF;

record: recordStart recordContent;

recordContent : LBRACE (contentItem | notRBrace | NEWLINE | WS)* RBRACE | ANY;

recordStart : RECORD_PRE TYPE RECORD_POST;

contentItem : recordContent;

notRBrace : ~RBRACE;

notRecord : .+?;
