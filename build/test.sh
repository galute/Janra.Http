#!/bin/bash

error() {
  echo ">>>>>> Test Failures Found, exiting test run <<<<<<<<<"
  echo ""

  exit 1
}

cleanup() {
  echo "....Cleaning up"
  # Remove any old containers (will not remove latest as they are still running)
  if [ $(docker ps -a |grep Exited | wc -c) -gt 0 ];then
  	docker ps -a |grep Exited | cut -f1 -d" " | ifne xargs docker rm
  fi

  # remove untagged images (these are left behind when docker run fails)
  if [ $(docker images | grep '^<none>' | wc -c) -gt 0 ]; then
    docker images | grep "^<none>" | tr -s " " " " | cut -f3 -d" " | ifne xargs docker rmi
  fi
  echo ""
  echo "....Cleaning up done"
}
trap error ERR
trap cleanup EXIT

ifne () {
        read line || return 1
        (echo "$line"; cat) | eval "$@"
}

CURDIR=`dirname $0`
pushd $CURDIR >/dev/null
ABS_CURDIR=`pwd`
popd >/dev/null

cleanup

echo ===========================================================
echo running TESTS
echo ===========================================================

docker-compose build bbtests

export TESTS_IMAGE=`docker images |grep bbtests | cut -f1 -d" "`

echo ===========================================================
echo running CTM.BuildingBlocks.Tests
echo ===========================================================

docker run --rm $TESTS_IMAGE nunit-console src/CTM.Common.BuildingBlocks.Tests/bin/Release/CTM.Common.BuildingBlocks.Tests.dll

echo ===========================================================
echo running CTM.Common.BuildingBlocks.Autofac.Tests
echo ===========================================================

docker run --rm $TESTS_IMAGE nunit-console src/CTM.Common.BuildingBlocks.Autofac.Tests/bin/Release/CTM.Common.BuildingBlocks.Autofac.Tests.dll


echo ===========================================================
echo running CTM.Common.BuildingBlocks.Json.Tests
echo ===========================================================

docker run --rm $TESTS_IMAGE nunit-console src/CTM.Common.BuildingBlocks.Json.Tests/bin/Release/CTM.Common.BuildingBlocks.Json.Tests.dll


echo ===========================================================
echo !!!!!!!!    ALL TESTS RUN SUCCESSFULLY    !!!!!!!!!!!!
echo ===========================================================
