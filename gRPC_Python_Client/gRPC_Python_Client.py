from __future__ import print_function
import logging

import grpc

from protobufGenerated import greet_pb2
from protobufGenerated import greet_pb2_grpc

from protobufGenerated import dataLoader_pb2
from protobufGenerated import dataLoader_pb2_grpc

def run_Hello():
    print("Hello to server!")
    # NOTE(gRPC Python Team): .close() is possible on a channel and should be
    # used in circumstances in which the with statement does not fit the needs
    # of the code.
    with grpc.insecure_channel('localhost:50051') as channel:
        stub = greet_pb2_grpc.GreeterStub(channel)
        response = stub.SayHello(greet_pb2.HelloRequest(name='you'))
    print("Greeter client received: " + response.message)


def run_DataLoader():
    print("Read data in one request!")
    with grpc.insecure_channel('localhost:50051') as channel:
        stub = dataLoader_pb2_grpc.DataLoaderStub(channel)
        response = stub.FetchData(dataLoader_pb2.TaskDataRequest(taskId="001", academicDisciplineId="Lab1"))
    print("\nData formt: " + response.dataFormat)
    print("Data: \n" + response.data)


def run_ServerStreaming():
    print("Read data in small pieces!")
    with grpc.insecure_channel('localhost:50051') as channel:
        stub = dataLoader_pb2_grpc.DataLoaderStub(channel)
        chunkNumber = 0
        for dataChunk in stub.FetchDataAsStream(dataLoader_pb2.TaskDataRequest(taskId="004", academicDisciplineId="Lab2")):
            chunkNumber = chunkNumber + 1
            print("Chunk " + str(chunkNumber) + ": " + dataChunk.data)


if __name__ == '__main__':
    logging.basicConfig()
    run_Hello()
    run_DataLoader()
    run_ServerStreaming()
