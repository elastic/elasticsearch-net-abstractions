#!/usr/bin/env bash
FAKE="packages/build/FAKE/tools/FAKE.exe"
BUILDSCRIPT="build/scripts/Targets.fsx"

if [[ -f .paket.lock ]]; then mono .paket/paket.exe restore; fi
if [[ ! -f .paket.lock ]]; then mono .paket/paket.exe install; fi
mono $FAKE $BUILDSCRIPT "cmdline=$*" --fsiargs -d:MONO 
