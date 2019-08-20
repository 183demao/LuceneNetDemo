## Lucene.Net 与 ES：

ES 基于 Lucene，并封装了 Lucene 的复杂性，同时增加了分布式部署的能力；



## Lucene.Net 使用流程：

1. 创建索引；
2. 解析搜索关键字；
3. 返回结果；



## Lucene.Net 约束：

Lucene.Net 需要将索引数据转换为 Document 对象，并将数据作为 Filed 保存在 Document 中。

而字段是 string 或 byte[]，不利于使用泛型使索引引用某个业务对象；

如果将业务对象序列化后存入文档，则需要考虑性能、循环引用、对象地址等问题；

